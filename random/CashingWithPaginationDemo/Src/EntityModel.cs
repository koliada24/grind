namespace CashingWithPaginationDemo.Src
{
    public class EntityModel
    {
        public static long StaticCounter { get; set; } = 0;
        
        public Guid Id { get; set; }
        public long Counter { get; set; } = 0;
        public string Value1 { get; set; } = string.Empty;
        public string Value2 { get; set; } = string.Empty;
        public string Value3 { get; set; } = string.Empty;
        public string Value4 { get; set; } = string.Empty;
        public string Value5 { get; set; } = string.Empty;
        public string Value6 { get; set; } = string.Empty;
        public string Value7 { get; set; } = string.Empty;
        public string Value8 { get; set; } = string.Empty;

        public static EntityModel Create()
        {
            StaticCounter++;

            return new EntityModel
            {
                Id = Guid.NewGuid(),
                Counter = StaticCounter,
                Value1 = "Value",
                Value2 = "Value",
                Value3 = "Value",
                Value4 = "Value",
                Value5 = "Value",
                Value6 = "Value",
                Value7 = "Value",
                Value8 = "Value"
            };
        }
    }
}
