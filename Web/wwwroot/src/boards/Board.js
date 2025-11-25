import './cells/Cell';

const template = document.createElement('template');
template.innerHTML = `
    <div class="container-fluid col-lg-8">
        <span>Waiting Player to Join</span>
        <div id="board">

        </div>
    </div>
`;

export class Board extends HTMLElement {
    
    #board = {
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
        this.render();
    }
    
    render() {
        this.replaceChildren(template.content.cloneNode(true));
        const board = this.querySelector("#board");
        const rows = this.#board.cells.length;
        const columns = this.#board.cells[0].length;
        let rowsTemplateHTML = '';
        for (let row = 0; row < rows; row++) {
            rowsTemplateHTML += `
                <div class="row text-center">
            `;
            for (let column = 0; column < columns; column++) {
                rowsTemplateHTML += `
                    <x-cell class="col p-0"></x-cell>
                `;
            }
            rowsTemplateHTML += `
                </div>
            `;
        }
        const rowsTemplate = document.createElement('template');
        rowsTemplate.innerHTML = rowsTemplateHTML;
        board.appendChild(rowsTemplate.content.cloneNode(true));
    }

    diconnectedCallback() {
    }
}

customElements.define('x-board', Board);
