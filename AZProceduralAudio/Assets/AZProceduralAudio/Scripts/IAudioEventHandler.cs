using UnityEngine;
using UnityEngine.EventSystems;

public interface IAudioEventHandler : IEventSystemHandler {
	void TriggerMessage (string name);
}
