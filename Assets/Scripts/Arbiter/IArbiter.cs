using System.Collections;

public interface IArbiter
{
	IEnumerator init ();
	IEnumerator updated ();
	void doAction (StateType _type, object _data);

	Character player { get; }
	Character rival { get; }
}