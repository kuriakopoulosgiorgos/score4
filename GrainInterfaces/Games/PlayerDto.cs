namespace GrainInterfaces.Games;

[GenerateSerializer, Immutable]
public record PlayerDto(Guid Id, string Name);