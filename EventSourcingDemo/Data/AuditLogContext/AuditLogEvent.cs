using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingDemo.Data.AuditLogContext;

public class AuditLogEvent
{
	public int Id { get; set; }
	public string EventType { get; set; }
	public DateTime Timestamp { get; set; }
	public string User { get; set; }
	public string EventData { get; set; }
}
