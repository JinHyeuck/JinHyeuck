using System.Collections;

public interface ISocket {
	void send(string _message);
	void receive(string _message);
}