using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventReplayController : ControllerBase
{
	
	readonly EventReplayService _eventReplayService;
	
	public EventReplayController(EventReplayService eventReplayService)
	{
		_eventReplayService = eventReplayService;
	}
	
	
	[HttpPost]
	public async Task<IActionResult> ReplayEvents()
	{
		await _eventReplayService.ReplayEventsAsync();
		
		return Ok("Events replaying initiated");
	}
}
