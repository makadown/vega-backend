using Microsoft.EntityFrameworkCore.Migrations;

namespace vegabackend.Migrations
{
    public partial class seedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT into makes(Name) Values ('Make 01') ");
            migrationBuilder.Sql("INSERT into makes(Name) Values ('Make 02') ");
            migrationBuilder.Sql("INSERT into makes(Name) Values ('Make 03') ");
            migrationBuilder.Sql("INSERT into makes(Name) Values ('Make 04') ");

/* Selecciono ID de makes en lugar de hardcodear el numero debido a que si llego a 
   hacer downgrade de la bd, el identity del nuevo id del nuevo registro de la tabla
   [makes] será diferente.
 */
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make1-ModelA', (Select Id from makes where name = 'Make 01')) ");
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make1-ModelB', (Select Id from makes where name = 'Make 01')) ");
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make1-ModelC', (Select Id from makes where name = 'Make 01')) ");

            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make2-ModelA', (Select Id from makes where name = 'Make 02')) ");
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make2-ModelB', (Select Id from makes where name = 'Make 02')) ");
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make2-ModelC', (Select Id from makes where name = 'Make 02')) ");

            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make3-ModelA', (Select Id from makes where name = 'Make 03')) ");
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make3-ModelB', (Select Id from makes where name = 'Make 03')) ");
            migrationBuilder.Sql("INSERT into Models(Name, MakeId) Values ('Make3-ModelC', (Select Id from makes where name = 'Make 03')) ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE from makes where name in ('Make 01', 'Make 02', 'Make 03', 'Make 04')");
            /* No se necesita por que esta cascadeado, 
              si se borra el make, se borran sus relacionados */
            // migrationBuilder.Sql("DELETE from models");
        }
    }
}
