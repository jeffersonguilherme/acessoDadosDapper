using DapperCurso.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace DapperCurso{
    class Program {
        static void Main(string[] args){
            const string connectionstring = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True";            
                
            
            using(var connection = new SqlConnection(connectionstring)){
                //UpdateCategory(connection);
                
                //CreateCategory(connection);
                //ListCategories(connection);
                //CreateAuthor(connection);
                //ListAuthor(connection);
                //Console.WriteLine("==================================================");
                //DeleteCategory(connection);
                //DeleteAuthor(connection);
                //UpdateAuthor(connection);
                
                //CreateManyAuthor(connection);
                //WriteLine("==================================================");
                //ListAuthor(connection);
                //ExecuteProcedure(connection);
                //ExecuteScalar(connection);
                OneToOne(connection);
            }
        }
    
        static void ListCategories(SqlConnection connection){
            
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
                foreach(var item in categories){
                    Console.WriteLine($"{item.Id} | {item.Title}");
                }
        }
    
        static void CreateCategory(SqlConnection connection){
                
                var category = new Category();

                category.Id = Guid.NewGuid();
                category.Title = "Amazon AWS";
                category.Url = "amazon";
                category.Description = "Categoria destinada a serviços do AWS";
                category.Order = 8;
                category.Summary = "AWS Cloud";
                category.Featured = false;

                var categoryDois = new Category();

                categoryDois.Id = Guid.NewGuid();
                categoryDois.Title = "API 2.0";
                categoryDois.Url = "API_2";
                categoryDois.Description = "Curso para criação de API DotNet";
                categoryDois.Order  = 9;
                categoryDois.Summary = "DOTNET API";
                categoryDois.Featured = false;


                var insertSql = @"INSERT INTO
                [Category]
                VALUES(
                    @Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

                    var rows = connection.Execute(insertSql, new {
                    category.Id, 
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured,
                });
                
                var row = connection.Execute(insertSql, new {
                    categoryDois.Id, 
                    categoryDois.Title,
                    categoryDois.Url,
                    categoryDois.Summary,
                    categoryDois.Order,
                    categoryDois.Description,
                    categoryDois.Featured,
                });

                Console.WriteLine($"{rows} linha inseridas");
                Console.WriteLine($"{row} linha inseridas");
        }

        static void UpdateCategory(SqlConnection connection){
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new {
                id = new Guid("208c96f3-6b28-4983-95a7-140bfc1da84d"),
                title = "Frontend 2021"});

                Console.WriteLine($"{rows} registros atualizadas");
        }

        static void DeleteCategory(SqlConnection connection){
            var deleteQuery = "DELETE FROM [Category] WHERE [Id]=@id";
            var row = connection.Execute(deleteQuery, new {
                id = new Guid("208c96f3-6b28-4983-95a7-140bfc1da84d")
            });
            Console.WriteLine($"{row} registros deletados");
        }
    
        static void ListAuthor(SqlConnection connection){
            var authores = connection.Query<Author>("SELECT [Id], [Name], [Title] FROM [Author]");
            foreach(var item in authores){
                Console.WriteLine($"{item.Id} | {item.Name} | {item.Title}");
            }
        }

        static void CreateAuthor(SqlConnection connection){

            var author = new Author();

            author.Id = Guid.NewGuid();
            author.Name = "Renata Santana";
            author.Title = "Dra. em biologia molecular";
            author.Image = "https://imagem_linda_de_perfil";
            author.Bio = "N/A";
            author.Url = "dra-renata-santana";
            author.Email = "renata.santana@hotmail.com.br";
            author.Type = 1;

            var insertSql = "INSERT INTO [Author] VALUES(@Id, @Name, @Title, @Image, @Bio, @Url, @Email, @Type)" ;
            
            connection.Execute(insertSql, new{
            author.Id,
            author.Name,
            author.Title,
            author.Image,
            author.Bio,
            author.Url,
            author.Email,
            author.Type,

            });

             
        }
    
        static void UpdateAuthor(SqlConnection connection){
            var updateQuery = "UPDATE [Author] SET [Title]=@title WHERE [Id]=@id";
            var row = connection.Execute(updateQuery, new {
                id = new Guid("bbccded2-bc83-4ba2-a860-c154bac817f8"),
                title = "Doutora em Biologia Molecular"
            });
        }
        static void DeleteAuthor(SqlConnection connection){
            var deleteQuery="DELETE FROM [Author] WHERE [Id]=@id";
            var row = connection.Execute(deleteQuery, new{
                id = new Guid("1b9a04ef-1381-43d2-8c2f-b929940e02b2")
            });
            Console.WriteLine($"{row} registros deletados");
        }
    
        static void CreateManyAuthor(SqlConnection connection){

            var author = new Author();
            author.Id = Guid.NewGuid();
            author.Name = "Jefferson Silva";
            author.Title = "Analista de Sistemas";
            author.Image = "https://imagem_linda_de_perfil";
            author.Bio = "N/A";
            author.Url = "jefferson-silva";
            author.Email = "jefferson.silva@hotmail.com.br";
            author.Type = 1;

            var author2 = new Author();
            author2.Id = Guid.NewGuid();
            author2.Name = "Jenneffer Silva";
            author2.Title = "Eng. Civil";
            author2.Image = "https://imagem_linda_de_perfil";
            author2.Bio = "N/A";
            author2.Url = "jenneffer-silva";
            author2.Email = "jenneffer.silva@hotmail.com.br";
            author2.Type = 1;

            var insertSql = "INSERT INTO [Author] VALUES(@Id, @Name, @Title, @Image, @Bio, @Url, @Email, @Type)" ;
            
            connection.Execute(insertSql, new[]{
            new{
                author.Id,
                author.Name,
                author.Title,
                author.Image,
                author.Bio,
                author.Url,
                author.Email,
                author.Type,
            },
            new{
                author2.Id,
                author2.Name,
                author2.Title,
                author2.Image,
                author2.Bio,
                author2.Url,
                author2.Email,
                author2.Type,
            }
            });

             
        }
    
       static void ExecuteProcedure(SqlConnection connection){
        var sql = "[spDeleteStudent]";
        var pars = new{ StudentId="36a40264-9b99-4e5d-8225-4e881691bb26"};
        var affectedRows = connection.Execute(sql, pars, commandType: CommandType.StoredProcedure);
        Console.WriteLine($"{affectedRows} linhas");
       } 
    
        static void ExecuteScalar(SqlConnection connection){
                
                var category = new Category();

                category.Title = "Amazon AWS";
                category.Url = "amazon";
                category.Description = "Categoria destinada a serviços do AWS";
                category.Order = 8;
                category.Summary = "AWS Cloud";
                category.Featured = false;


                var insertSql = @"INSERT INTO
                [Category]
                OUTPUT inserted.[Id]
                VALUES(
                    NEWID(),
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured) SELECT SCOPE_IDENTITY()";

                    var id = connection.ExecuteScalar<Guid>(insertSql, new { 
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured,
                });
                

                Console.WriteLine($"A categoria inserida foi : {id}");
        }
    
        static void OneToOne(SqlConnection connection){
            var sql = "SELECT * FROM [CareerItem] INNER JOIN [Course] ON [CareerItem].[CourseId] = [Course].[Id]";
            var items = connection.Query<CareerItem, Course, CareerItem>(
                sql,
                (careerItem, course)=>{
                    careerItem.Course = course;
                    return careerItem;
                }, splitOn: "Id");

            foreach(var item in items){
                Console.WriteLine($"{item.Title} | Curso: {item.Course.Title}");
            }
        }
    }
}