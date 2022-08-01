using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualShop.Products.API.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO products (name,price,description,stock,imageurl,categoryid) VALUES ('Caderno', 7.55,  'Caderno', 10, 'caderno1.jpg', 1)");
            mb.Sql("INSERT INTO products (name,price,description,stock,imageurl,categoryid) VALUES ('Lapis', 3.45,  'Lapis preto', 20, 'lapis1.jpg', 1)");
            mb.Sql("INSERT INTO products (name,price,description,stock,imageurl,categoryid) VALUES ('Clips', 5.33,  'Clips para papel', 50, 'clips1.jpg', 2)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM products");
        }
    }
}
