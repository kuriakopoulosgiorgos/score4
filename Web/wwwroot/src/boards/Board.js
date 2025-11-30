import './cells/Cell.js';

const template = document.createElement('template');
template.innerHTML = `
    <div class="row">
        <div class="container-fluid col-lg-7 p-0">
            <div class="card bg-body-tertiary">
                <div class="card-header">Player Playing: <span id="boardPlayerPlaying"/></div>
                <div class="card-body">
                    <h4>Score</h4>
                    <div class="btn-group" role="group" aria-label="Basic mixed styles example">
                        <button id="playerOneScore" type="button" class="btn btn-primary"></button>
                        <button id="playerTwoScore" type="button" class="btn btn-danger"></button>
                    </div>
                    <hr/>
                    <h5 class="card-title">Game Status: <span id="boardGameStatus"/></h5>
                    <p class="card-text">Board Status: <span id="boardBoardStatus"/></p>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="container-fluid col-lg-7">    
            <div id="board" class="shadow-lg bg-body-tertiary rounded">

            </div>
        </div>
    </div>
    
    <div id="roundFinishedModal" class="modal fade" aria-labelledby="roundFinishedModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="roundFinishedModal">Round Finished</h1>
                </div>
                <div class="modal-footer">                    
                    <button id="playAgainSubmitButton" type="button" class="btn btn-primary">Play Again</button>
                    <button id="exitGameSubmitButton" type="button" class="btn btn-danger" data-bs-dismiss="modal">Exit Game</button>
                </div>
            </div>
        </div>
    </div>
`;

export class Board extends HTMLElement {
    
    #player = null;
    #gameUpdate = {
        "playerOneScore": 0,
        "playerTwoScore": 0,
        "roomName": "",
        "cells": [
            [
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                }
            ],
            [
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                }
            ],
            [
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                }
            ],
            [
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                }
            ],
            [
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                }
            ],
            [
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                },
                {
                    "value": 0
                }
            ]
        ],
        "playerPlaying": null,
        "boardStatus": 1,
        "gameStatus": 1
    };

    constructor() {
        super();
    }

    connectedCallback() {
        window.addEventListener("OnGameUpdated", this.onGameUpdated.bind(this));
        this.render();
    }
    
    set player(player) {
        this.#player = player;
    }
    
    get player() {
        return this.#player;
    }
    
    get yourTurn() {
        return this.#player && this.#player.id === this.#gameUpdate.playerPlaying?.id;
    }
    
    render() {
        this.replaceChildren(template.content.cloneNode(true));
        const boardPlayerPlaying = this.querySelector("#boardPlayerPlaying");
        boardPlayerPlaying.textContent = this.#gameUpdate.playerPlaying?.name;
        if (this.yourTurn){
            boardPlayerPlaying.innerHTML += ' <span class="badge rounded-pill text-bg-success">Your Turn</span>';
        }
        this.querySelector("#playerOneScore").textContent = this.#gameUpdate.playerOneScore;
        this.querySelector("#playerTwoScore").textContent = this.#gameUpdate.playerTwoScore;
        this.querySelector("#boardGameStatus").textContent = this.gameStatus;
        this.querySelector("#boardBoardStatus").textContent = this.boardStatus;
        this.querySelector("#board");
        const board = this.querySelector("#board");
        const rows = this.#gameUpdate.cells.length;
        const columns = this.#gameUpdate.cells[0].length;
        let rowsTemplateHTML = '';
        for (let row = rows -1; row >= 0; row--) {
            rowsTemplateHTML += `
                <div class="row text-center">
            `;
            for (let column = 0; column < columns; column++) {
                const cellValue = this.#gameUpdate.cells[row][column].value;
                const color = this.colorFromCellValue(cellValue);
                rowsTemplateHTML += `
                    <x-cell id="${(row * columns) + column}" color="${color}" class="col p-0"></x-cell>
                `;
            }
            rowsTemplateHTML += `
                </div>
            `;
        }
        const rowsTemplate = document.createElement('template');
        rowsTemplate.innerHTML = rowsTemplateHTML;
        board.appendChild(rowsTemplate.content.cloneNode(true));
        this.querySelectorAll('x-cell')
            .forEach(cell =>
            {
                cell.addEventListener('click', this.onCellClick.bind(this));
            });

        let roundFinishedModal = bootstrap.Modal.getOrCreateInstance(this.querySelector("#roundFinishedModal"), {backdrop: false});
        roundFinishedModal.hide();
       
        if (this.gameStatus === 'Finished'){
            roundFinishedModal.show();
            const playAgainSubmitButton = this.querySelector('#playAgainSubmitButton');
            playAgainSubmitButton.addEventListener('click', this.onPlayAgainClick.bind(this));
            const exitGameSubmitButton = this.querySelector('#exitGameSubmitButton');
            exitGameSubmitButton.addEventListener('click', this.onExitGameClick.bind(this));
        }

    }

    set gameUpdate(gameUpdate) {
        this.#gameUpdate = gameUpdate;
        this.render();
    }
    
    get gameStatus() {
        return ({
            1: 'Waiting Player To Join',
            2: 'Playing',
            3: 'Finished',
        })[this.#gameUpdate.gameStatus];
    }

    get boardStatus() {
        return ({
            1: 'Available Moves',
            2: 'Player 1 Won',
            3: 'Player 2 Won',
            4: 'Draw',
        })[this.#gameUpdate.boardStatus];
    }
    
    colorFromCellValue(cellValue) {
        return ({
            0: 'white',
            1: 'blue',
            2: 'red',
        })[cellValue];
    }
    
    onGameUpdated(gameUpdateEvent) {
        console.log("board gameUpdate");
        console.log(gameUpdateEvent);
        this.gameUpdate = gameUpdateEvent.detail;
    }
    
    onCellClick(cellClickEvent) {
        if (!this.yourTurn) {
            return;
        }
        const column = this.getColumnNumber(cellClickEvent.target.id);
        this.dispatchEvent(
            new CustomEvent('onPlaceCell', {
                detail: { column: column },
                bubbles: true,
                composed: true
            })
        );
    }

    onPlayAgainClick() {
        this.dispatchEvent(
            new CustomEvent('onPlayAgain', {
                detail: { roomName: this.#gameUpdate.roomName },
                bubbles: true,
                composed: true
            })
        );
    }

    onExitGameClick() {
        this.dispatchEvent(
            new CustomEvent('onExitGame', {
                bubbles: true,
                composed: true
            })
        );
    }
    
    getColumnNumber(cellId) {
        const columns = this.#gameUpdate.cells[0].length;
        return cellId % columns;
    } 

    diconnectedCallback() {
        window.removeEventListener("OnGameUpdated", this.onGameUpdated.bind(this));
    }
}

customElements.define('x-board', Board);
