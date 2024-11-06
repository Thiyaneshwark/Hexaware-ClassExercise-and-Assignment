using DependencyInversionPrinciple_Demo;


// Dependency Inversion Priniciple states that High Level Module/classes.
// should not depend on Low—levet Module/ctasses.
//Both Should depend upon Abstraction. s (interface , abstract class)
//Abstractions should not depend upon details but details should depend upon abstractions.

var players = new Dictionary<string, IAudioPlayer>
{
    {"MP3",new MP3Player() },
    {"WAV",new WAVPlayer() }
};

var musicPlayer=new MusicPlayer(players);
musicPlayer.Play("song1.mp3", "MP3");
musicPlayer.Play("song2.wav", "WAV");
musicPlayer.Play("song3.mp4", "MP4");


Console.ReadLine();