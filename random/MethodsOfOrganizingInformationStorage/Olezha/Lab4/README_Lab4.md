# Lab 4: Programming Languages Full-Text Search Implementation

## Overview
Розширення системи пошуку мов програмування з Lab3 для підтримки повнотекстового пошуку за допомогою Elasticsearch.

## Вимоги Lab 4 - Виконано ✅

### 1. Структура документа (ProgrammingLanguage)
- ✅ 3 нових text поля додано:
  - `Description` - опис мови програмування (standard analyzer)
  - `Documentation` - документація (english analyzer)
  - `CommunityFeedback` - відгуки спільноти (custom analyzer)

### 2. Аналізатори

#### Поле 1: Description
- **Аналізатор:** standard
- **Компоненти:** standard tokenizer + lowercase filter
- **Призначення:** базовий повнотекстовий пошук опису мови

#### Поле 2: Documentation  
- **Аналізатор:** english
- **Компоненти:** standard tokenizer + lowercase + stop filter + english stemmer
- **Призначення:** стеммінг та видалення стоп-слів для технічної документації

#### Поле 3: CommunityFeedback
- **Аналізатор:** custom_feedback_analyzer (користувацький)
- **Компоненти:** html_strip filter + standard tokenizer + lowercase + stop filter
- **Призначення:** обробка HTML та видалення стоп-слів з відгуків

### 3. Повнотекстові запити
- ✅ Реалізовано **match запит** для кожного поля
- Режим: OR (за замовчуванням) - документ повертається, якщо містить хоча б один токен запиту

## Як використовувати

### Додавання документа

```
Оберіть: 1
Name: Python
Year created: 1991
Created by: Guido van Rossum
Current version: 3.12
Popularity (users): 10000000
Website: https://www.python.org
Description: Python is a high-level, interpreted programming language known for its simplicity and readability. It supports multiple programming paradigms.
Documentation (text): Python documentation provides comprehensive guides for beginners and advanced users. It includes tutorials, libraries, and API references.
Community feedback (text): Amazing language for beginners! <b>Highly recommended</b> for data science and web development. Great community support and extensive libraries.
```

### Пошук за текстом (Опція 6-8)

#### Пошук в Description (standard analyzer)
```
Оберіть: 6
Search in description (query): high-level interpreted
```
Знайде документи, що містять "high-level" АБО "interpreted"

#### Пошук в Documentation (english analyzer)
```
Оберіть: 7
Search in documentation (query): tutorials libraries comprehensive
```
Знайде документи з будь-яким з цих слів (з урахуванням стеммінгу)

#### Пошук в Community Feedback (custom analyzer)
```
Оберіть: 8
Search in community feedback (query): amazing data science
```
Знайде документи, очищені від HTML та стоп-слів

## Кодова структура

### ProgrammingLanguage.cs
- 7 полів типу keyword/integer/long
- 3 нові поля типу text з [JsonPropertyName] атрибутами

### Program.cs
- SearchDescription() - пошук за описом мови
- SearchDocumentation() - пошук за документацією
- SearchCommunityFeedback() - пошук за відгуками

### ElasticsearchService.cs
- Налаштовано JsonSerializerOptions з camelCase

## Тестові дані

```json
{
  "name": "Python",
  "yearCreated": 1991,
  "createdBy": "Guido van Rossum",
  "currentVersion": "3.12",
  "popularity": 10000000,
  "website": "https://www.python.org",
  "description": "High-level interpreted programming language for simplicity",
  "documentation": "Comprehensive guides and API references for Python",
  "communityFeedback": "Amazing language <b>highly recommended</b> for data science"
}
```

## Запити SQL еквіваленти

### Description пошук
```
SELECT * FROM programming_languages 
WHERE description MATCH 'high level'
```

### Documentation пошук (з стеммінгом)
```
SELECT * FROM programming_languages 
WHERE documentation CONTAINS ('tutorial' OR 'libraries' OR 'comprehensive')
AND language='english'
```

### Community Feedback пошук
```
SELECT * FROM programming_languages 
WHERE community_feedback MATCH 'amazing data science' 
WITH html_filtering AND stopword_removal
```
