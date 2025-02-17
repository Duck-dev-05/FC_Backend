using Microsoft.AspNetCore.Mvc;
using FootballClubBE.Models;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FootballClubBE.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public MatchesController()
        {
            _firestoreDb = FirestoreDb.Create("footballclubwebsite"); // Replace with your Firebase project ID
        }

        [HttpGet]
        public async Task<ActionResult<List<Match>>> GetAll()
        {
            var matches = new List<Match>();
            var snapshot = await _firestoreDb.Collection("matches").GetSnapshotAsync();
            foreach (var document in snapshot.Documents)
            {
                var match = document.ConvertTo<Match>();
                matches.Add(match);
            }
            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetById(string id)
        {
            var document = await _firestoreDb.Collection("matches").Document(id).GetSnapshotAsync();
            if (!document.Exists)
            {
                return NotFound();
            }
            var match = document.ConvertTo<Match>();
            return Ok(match);
        }

        [HttpPost]
        public async Task<ActionResult<Match>> Create(Match match)
        {
            match.Id = Guid.NewGuid().ToString(); // Generate a new ID
            await _firestoreDb.Collection("matches").Document(match.Id).SetAsync(match);
            return CreatedAtAction(nameof(GetById), new { id = match.Id }, match);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Match updatedMatch)
        {
            var document = await _firestoreDb.Collection("matches").Document(id).GetSnapshotAsync();
            if (!document.Exists)
            {
                return NotFound();
            }

            await _firestoreDb.Collection("matches").Document(id).SetAsync(updatedMatch);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var document = await _firestoreDb.Collection("matches").Document(id).GetSnapshotAsync();
            if (!document.Exists)
            {
                return NotFound();
            }

            await _firestoreDb.Collection("matches").Document(id).DeleteAsync();
            return NoContent();
        }
    }
} 