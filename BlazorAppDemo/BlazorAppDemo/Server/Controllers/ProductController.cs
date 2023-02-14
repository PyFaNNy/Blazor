using Microsoft.AspNetCore.Mvc;

namespace BlazorAppDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Product> Products = new List<Product>
    {
        new Product
        {
            Id = 1,
            Title = "Anna Karenina",
            Description =
                "Anna Karenina (Russian: «Анна Каренина», IPA: [ˈanːə kɐˈrʲenʲɪnə])[1] is a novel by the Russian author Leo Tolstoy, first published in book form in 1878. Widely considered to be one of the greatest works of literature ever written,[2] Tolstoy himself called it his first true novel. It was initially released in serial installments from 1875 to 1877, all but the last part appearing in the periodical The Russian Messenger.[3]",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/AnnaKareninaTitle.jpg/800px-AnnaKareninaTitle.jpg",
            Price = 9.99m
        },
        new Product
        {
            Id = 2,
            Title = "To Kill a Mockingbird",
            Description =
                "To Kill a Mockingbird is a novel by the American author Harper Lee. It was published in 1960 and was instantly successful. In the United States, it is widely read in high schools and middle schools. To Kill a Mockingbird has become a classic of modern American literature, winning the Pulitzer Prize. The plot and characters are loosely based on Lee's observations of her family, her neighbors and an event that occurred near her hometown of Monroeville, Alabama, in 1936, when she was ten.",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg/800px-To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg",
            Price = 7.99m
        },
        new Product
        {
            Id = 3,
            Title = "The Great Gatsby",
            Description =
                "The Great Gatsby is a 1925 novel by American writer F. Scott Fitzgerald. Set in the Jazz Age on Long Island, near New York City, the novel depicts first-person narrator Nick Carraway's interactions with mysterious millionaire Jay Gatsby and Gatsby's obsession to reunite with his former lover, Daisy Buchanan.",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg/800px-The_Great_Gatsby_Cover_1925_Retouched.jpg",
            Price = 8.99m
        }
    };

    [HttpGet]
    public async Task<IActionResult> GetProduct()
    {
        return Ok(Products);
    }
}