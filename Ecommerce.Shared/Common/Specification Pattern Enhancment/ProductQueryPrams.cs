namespace Ecommerce.Shared.Common.Specification_Pattern_Enhancment
{
    public class ProductQueryPrams
    {
        //validaations on pagination 
        private const int DefaultSize = 5;
        private const int MaxSize = 10;

        //kol ma a3oz azwed param lazm a3oz azwed property hena 3shan a3oz a2dr a2bl el params di mn el service w a3ml 7aga b3dha
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingWay? SortingWay { get; set; }

        public string? SearchValue { get; set; }



        //pagination validations 

        //lazem ab3t el hagat de w astlemha mn el controller
        public int PageIndex { get; set; } = 1;//incase the front end did not send any index or size so we used a default = 1
        //full property 
        private int PageSize = DefaultSize;

        public int pageSize
        {
            get
            {
                return PageSize;
            }
            set
            {           // el mtbasya mn el controller
                PageSize = value > MaxSize ? MaxSize : value;
            }
        }




    }
}
