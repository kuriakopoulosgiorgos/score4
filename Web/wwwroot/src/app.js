import { signalR } from './libs/signalR.js';
import './players/CreatePlayer.js';
import './games/CreateJoinGame.js';
import './board/Board.js';

const template = document.createElement('template');
template.innerHTML = /*html*/`
    <div id="outlet"></div>

    <template id="connectingTemplate">
        <div style="text-align: center;">
            <p>Loading...</p>
            <div class="spinner-border"/>
        </div>
    </template>

    <template id="createPlayerTemplate">
        <x-create-player></x-create-player>
    </template>

    <template id="createJoinGameTemplate">
        <x-create-join-game></x-create-join-game>
    </template>

    <template id="boardTemplate">
        <x-board></x-board>
    </template>

    <x-board></x-board>

    <div id="errorToast"  class="toast align-items-center text-bg-danger position-absolute bottom-0 end-0" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
            <div id="errorMessage" class="toast-body">
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    </div>
`;

export class App extends HTMLElement {

    constructor() {
        super();
        this.replaceChildren(template.content.cloneNode(true));
    }

    connectedCallback() {
        this.renderTemplate = 'connectingTemplate';
        this.render();
    }

    render() {
        const renderTemplate = this.querySelector(`#${this.renderTemplate}`).content.cloneNode(true);
        this.querySelector("#outlet").replaceChildren(renderTemplate);
    }

    displayError(message)
    {
        const errorToast = this.querySelector('#errorToast');
        this.querySelector('#errorMessage').textContent = message;
        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(errorToast)
        toastBootstrap.show();
    }
}

customElements.define('x-app', App);

const appComponent = document.querySelector('#app');

window.addEventListener("onConnected", () => {
    appComponent.renderTemplate = 'createPlayerTemplate';
    appComponent.render();
});

window.addEventListener("onPlayerCreate", async (onPlayerCreateEvent) => {
    await createPlayer(onPlayerCreateEvent.detail.playerName);
});

window.addEventListener("onCreateGame", async (onCreateGameEvent) => {
    await createJoinGame(onCreateGameEvent.detail.roomName, "CreateGame");
});

window.addEventListener("onJoinGame", async (onJoinGameEvent) => {
    await createJoinGame(onJoinGameEvent.detail.roomName, "JoinGame");
});

window.addEventListener("onGameCreatedJoined", () => {
    // Change view to Board, initialize the board to empty cells
});

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();


async function start() {
    try {
        await connection.start();
        dispatchEvent(new Event("onConnected"));
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function createPlayer(playerName) {
    try {
        let player = await connection.invoke("CreatePlayer", playerName);
        console.log(player);
        appComponent.renderTemplate = 'createJoinGameTemplate';
        appComponent.render();
    } catch (err) {
        console.error(err);
    }
};

async function createJoinGame(roomName, method) {
    try {
        await connection.invoke(method, roomName);
        appComponent.renderTemplate = 'boardTemplate';
        appComponent.render();
    } catch (err) {
        console.error(err);
    }
};

connection.onclose(async () => {
    await start();
});

connection.on("OnGameException", async (errorMessage) => {
    appComponent.displayError(errorMessage);
});

connection.on("OnGameUpdated", async (gameUpdated) => {
    console.log(gameUpdated)
});

start();