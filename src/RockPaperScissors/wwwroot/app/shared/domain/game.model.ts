export class Game {
    Id: number;
    Player1Name: string;
    Player2Name: string;
    Settings: string;

    constructor(id: number,
        player1Name: string,
        player2Name: string,
        settings: string) {
        this.Id = id;
        this.Player1Name = player1Name;
        this.Player2Name = player2Name;
        this.Settings = settings;
    }
}