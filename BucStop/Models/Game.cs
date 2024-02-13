using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

/*
 * This file contains the Game class, which holds necessary information
 * about the games. Used in GamesController.cs, which has actual instances
 * of each game.
 */

namespace BucStop.Models
{
    public class Game
    {
        //The ID of the game. Starts at 1.
        public int Id { get; set; }

        public GameInfo[] Info { get; set; }

        //The title/name of the game.
        [Required]
        public string Title { get; set; }

        //The javascript file for the game
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        //The author(s) of the game.
        [Required]
        public string Author { get; set; }
        //Shows the Date the game was added
        [Required]
        public string DateAdded { get; set; }

        //The description of the game.
        [Required]
        public string Description { get; set; }

        //An explanation of how to play the game, including controls.
        [Required]
        public string HowTo { get; set; }

        //The link to the image of the thumbnail.
        [Required]
        public string Thumbnail { get; set; }

        public int PlayCount { get; set; }

        /*public async Task OnGet([FromServices] MicroClient microClient)
        {
            Info = await microClient.GetGamesAsync  ();
        } */
    }
}
