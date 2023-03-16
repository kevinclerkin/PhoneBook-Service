using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using PhoneBookService.Models;
using System.Collections;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {

        private static List<Contacts> phonebook = new List<Contacts>()
        {
            new Contacts() {Name = "David Boyle", PhoneNumber = "175321", Address = "175 Yellow Street"},
            new Contacts() {Name = "David Davis", PhoneNumber = "175684", Address = "321 Bond Street"},
            new Contacts() {Name = "Alex Ferguson", PhoneNumber = "196925", Address = "444 Oak Drive"},
            new Contacts() {Name = "Roy Keane", PhoneNumber = "189822", Address = "1343 Mulholland Drive"}
        };

        [HttpGet("GetAll")]
        public IEnumerable<Contacts> GetAll()
        {
            return phonebook;

        }




        [HttpGet("GetNameAndAddress/{phoneNumber}")]
        public IActionResult GetNameAndAddress(string phoneNumber)
        {
            var result = phonebook.Where(p => p.PhoneNumber == phoneNumber).Select(p => new { p.Name, p.Address }).ToList();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);

        }

        [HttpGet("GetNumberAndAddress/{name}")]
        public IActionResult GetNumberAndAddress(string name)
        {
            var resultFromName = phonebook.Where(p => p.Name == name).Select(p => new { p.PhoneNumber, p.Address }).ToList();

            if (resultFromName == null)
            {
                return NotFound();
            }

            return Ok(resultFromName);

        }

        [HttpGet("GetContactFromPrefix/{firstLetterName}")]

        public IActionResult GetContactFromPrefix(string firstLetterName)
        {
            var nameBeginsWith = phonebook.Where(n => n.Name.StartsWith(firstLetterName, StringComparison.OrdinalIgnoreCase)).ToList();
            if(nameBeginsWith.Count > 0)
            {
                return Ok(nameBeginsWith);
            }
            return NotFound();
        }
        
           
        

        [HttpPost]
        public IActionResult Post([FromBody] Contacts contact)
        {
            Contacts found = phonebook.FirstOrDefault(Contact => Contact.Name == contact.Name);
            if (found == null)
            {
                phonebook.Add(contact);
                return Ok();
               

            }
            return BadRequest();



        }
        [HttpPut("{newNumber}")]
        public IActionResult Put(string newNumber, [FromBody] Contacts contact)
        {
            Contacts found = phonebook.FirstOrDefault(Contact => Contact.Name == contact.Name);
            if (found != null)
            {
                found.PhoneNumber = newNumber;
                return Ok();
            }
            return NotFound();



        }

        [HttpDelete("DeleteYellow")]
        public IActionResult DeleteYellow(){
        //{
            //Contacts foundYellow = (Contacts)phonebook.Where(a => a.Address.Contains("Yellow")).ToList();
            //if(foundYellow != null)
            //{
                //phonebook.Remove(foundYellow);
                //return Ok();
            //}
            //return NotFound();  
            
            
            
            
            
            foreach (Contacts contact in phonebook)
            {
                if (contact.Address.Contains("Yellow"))
                {
                    phonebook.Remove(contact);
                    return Ok($"Removed {contact}");
                }


            }
            return BadRequest(string.Empty);
        }

        [HttpDelete("DeleteNumber/{delNumber}")]
        public IActionResult DeleteNumber(string delNumber)
        {
            Contacts foundNum = phonebook.FirstOrDefault(n => n.PhoneNumber == delNumber);
            if (foundNum != null)
            {
                phonebook.Remove(foundNum);
                return Ok("Removed Phone Number " + foundNum.ToString());
                
            }
            return BadRequest($"Phone Number:{delNumber} not found");
            
        }

            /*[HttpPut("Put")]
            public IActionResult Put(Contacts name)
            {
                if (n => n.Name == 
                {
                    phonebook.Add(Name);
                    return Ok();
                }

                return BadRequest();

            }*/












        
    }
}
