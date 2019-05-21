using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using cmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //declarando la var para la inyeccion de dependencia
        private readonly CommandContext _con;

        //inyeccion de dependencia utilizando 'arrow function'
        public CommandsController(CommandContext context) => _con = context;


        //GET api/commands
        //Retorna lista de comandos
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands ()
        {
            return _con.CommandItems;
        }

        //GET:  api/commands/id
        //Retorna un comando en especifico
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var commandItems = _con.CommandItems.Find(id);

            if (commandItems == null)
            {
                return NotFound();
                
            }

            return commandItems;
        }

        //POST: api/commands
        //Crea un comando
        [HttpPost]
        public ActionResult<Command> CreateCommand(Command command)
        {
            _con.CommandItems.Add(command);
            _con.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command{Id = command.Id}, command);
        }

        //PUT: api/commands/id
        //Actualiza un commando
        [HttpPut("{id}")]
        public ActionResult PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            _con.Entry(command).State = EntityState.Modified;
            _con.SaveChanges();

            return NoContent();


        }

        //DELETE: api/commands/id
        //Borra un comando
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _con.CommandItems.Find(id);

            if (commandItem == null)    
            {
                return NotFound();
                
            }

            _con.CommandItems.Remove(commandItem);
            _con.SaveChanges();

            return commandItem;

        }


    }
}