export class Player {
    Id: number;
    RoundNo: number;
    Move1: string;
    Move2: string;
    GameId: number;

    constructor(id: number,
        roundNo: number,
        move1: string,
        move2: string,
        gameId: number) {

        this.Id = id;
        this.RoundNo = roundNo;
        this.GameId = gameId;
        this.Move1 = move1;
        this.Move2 = move2;
    }
}