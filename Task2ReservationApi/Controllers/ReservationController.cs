using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace Task2ReservationApi.Controllers;

public record Reservation(int Id, string Name, DateTime Start, DateTime End);

public record ReservationCreateRequest(
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    [RegularExpression("^[A-Za-z]+(?: [IVXLCDM]+)?$", ErrorMessage = "Name must contain latin letters in upper or lower case and can contain Rome numbers in the end of the name.")]
    string Name,
    [Required]
    DateTime Start, 
    [Required]
    DateTime End
    );

[ApiController]
[Route("[controller]s")]
public class ReservationController : ControllerBase
{
    private static List<Reservation> _db = new(100);
    
    [HttpPost]
    public IActionResult Create([FromBody]ReservationCreateRequest? request)
    {
        if (request is not null)
        {
            var entity = new Reservation(Random.Shared.Next(0, 101), request.Name, request.Start, request.End);
            
            _db.Add(entity);

            var lastAddedReservation = _db[_db.Count - 1];
            
            return Ok(lastAddedReservation);
        }
        
        return BadRequest();
    }
}