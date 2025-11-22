namespace GrainInterfaces.Games;

[GenerateSerializer, Immutable]
public record PlayerDto(string Id, string Name);