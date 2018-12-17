#LightCosmosRat
Light Cosmos Rat is a RAT(Remote Administration Tool) for Windows developed in C#. It needs .Net Framework and/or Mono to be executed.
The program consist of two sections:
- The Listening section
- The Create Client section

1. The listening section allows you to listen on your local port for an incoming connection, generally you choose the port that you put
in the client during its creation, after connection is established you will see the other person screen on the listening panel.

2. The Create Client section allows you to create a Client, you can choose the name of the .exe file that you'll generate. The ip and the port that the client will connect to. After the client runs for the first time on a machine, it copies itself in the startup folder and then tries forever to establish a connection with the server(the listening panel running on the other computer), after the connection is established, the client will send desktop images forever, unless the connection is interrupted or lost, in this case it will try to re-establish the connection as soon as possibile.
