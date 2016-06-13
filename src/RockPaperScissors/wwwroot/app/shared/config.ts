
export default const CONFIG = {
    charactersUrl: 'api/characters',
    DEFAULT_GAME_SETTINGS: {
        "wins": 3,
        "moves": [

            {
                "name": "paper",
                "beats": "rock"
            },
            {
                "name": "rock",
                "beats": "scissors"
            },
            {
                "name": "scissors",
                "beats": "paper"
            }
        ]
    }
}