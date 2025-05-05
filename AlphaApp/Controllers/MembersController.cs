using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaApp.Controllers;

[Authorize(Policy ="Admins")]
public class MembersController : Controller
{
    [HttpPost]
    public IActionResult Add(AddMemberForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );
            return BadRequest(new { success = false, errors });
        }

        return Ok(new { success = true });


    }


    [HttpPost]
    public IActionResult Edit(EditMemberForm form)
    {

        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );
            return BadRequest(new { success = false, errors });
        }

        return Ok(new { success = true });

    }
}

