# Lab 4: Theatre Full-Text Search Implementation

## Overview
Розширення системи театральної пошуку з Lab3 для підтримки повнотекстового пошуку за допомогою Elasticsearch.

## Вимоги Lab 4 - Виконано ✅

### 1. Структура документа (Theatre)
- ✅ 3 нових text поля додано:
  - `Description` - опис вистави (standard analyzer)
  - `CastReviews` - рецензії акторів (english analyzer)
  - `Synopsis` - короткий сюжет (custom analyzer)

### 2. Аналізатори

#### Поле 1: Description
- **Аналізатор:** standard
- **Компоненти:** standard tokenizer + lowercase filter
- **Призначення:** базовий повнотекстовий пошук

#### Поле 2: CastReviews  
- **Аналізатор:** english
- **Компоненти:** standard tokenizer + lowercase + stop filter + english stemmer
- **Призначення:** стеммінг та видалення стоп-слів для англійського тексту

#### Поле 3: Synopsis
- **Аналізатор:** custom_synopsis_analyzer (користувацький)
- **Компоненти:** html_strip filter + standard tokenizer + lowercase + stop filter
- **Призначення:** обробка HTML та видалення стоп-слів

### 3. Повнотекстові запити
- ✅ Реалізовано **match запит** для кожного поля
- Режим: OR (за замовчуванням) - документ повертається, якщо містить хоча б один токен запиту

## Як використовувати

### Додавання документа

```
Оберіть: 1
Title: Гамлет
Author: Вільям Шекспір
Year premiere: 1603
Director: Костянтин Станіславський
Main actor: Іван Мозжухін
Rating (1-10): 10
Website: http://example.com
Description: Трагедія про данського принца, який шукає помсту. Класичний твір світової драматургії.
Cast reviews: The performance was outstanding and memorable. Incredible acting by the lead character.
Synopsis: Гамлет дізнається про вбивство свого батька і планує помсту.
```

### Пошук за текстом (Опція 6-8)

#### Пошук в Description (standard analyzer)
```
Оберіть: 6
Search in description (query): трагедія данського
```
Знайде документи, що містять слова "трагедія" АБО "данського"

#### Пошук в Cast Reviews (english analyzer)
```
Оберіть: 7
Search in cast reviews (query): outstanding performance
```
Знайде документи з "outstanding" АБО "performance" (з урахуванням стеммінгу)

#### Пошук в Synopsis (custom analyzer)
```
Оберіть: 8
Search in synopsis (query): батька помсту
```
Знайде документи, очищені від HTML та стоп-слів

## Кодова структура

### Theatre.cs
- 7 полів типу keyword/integer
- 3 нові поля типу text з [JsonPropertyName] атрибутами

### Program.cs
- SearchDescription() - пошук за описом
- SearchCastReviews() - пошук за рецензіями
- SearchSynopsis() - пошук за сюжетом

### ElasticsearchService.cs
- Налаштовано JsonSerializerOptions з camelCase

## Тестові дані

```json
{
  "title": "Гамлет",
  "author": "Вільям Шекспір",
  "yearPremiere": 1603,
  "director": "К. Станіславський",
  "mainActor": "І. Мозжухін",
  "rating": 10,
  "website": "http://example.com",
  "description": "Трагедія про данського принца",
  "castReviews": "Outstanding and memorable performance",
  "synopsis": "Гамлет dіznajose про вбивство батька"
}
```
