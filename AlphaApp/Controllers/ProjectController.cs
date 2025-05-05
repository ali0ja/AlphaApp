using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaApp.Controllers;
[Authorize]
public class ProjectController : Controller
{
    [HttpPost]
    public IActionResult Add(AddProjectForm form)
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
    public IActionResult Edit(EditProjectForm form)
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

