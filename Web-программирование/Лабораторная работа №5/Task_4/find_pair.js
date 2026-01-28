import { attributes, cls, div, events, input, option, select, slot, style } from "./elements.mjs"

// Конфигурация игры
const config = {
    sizes: [
        { name: "4x4 (8 пар)", value: 4 },
        { name: "6x6 (18 пар)", value: 6 },
        { name: "8x8 (32 пар)", value: 8 }
    ],
    themes: {
        animals: ['🐭', '🐹', '🐰', '🐵', '🦍', '🐻', '🐻‍❄️', '🐨', '🐼', '🦥', '🦧', '🐶', '🐕', '🐩', '🐺', '🦊', '🦝', '🐱', '🦁', '🐯', '🦄', '🐮', '🐷', '🐘', '🐇', '🐿️', '🦫', '🦔', '🦇', '🦦', '🦨', '🦘'],
        fruits: ['🍇', '🍈', '🍉', '🍊', '🍋', '🍌', '🍍', '🥭', '🍎', '🍏', '🍐', '🍑', '🍒', '🍓', '🫐', '🥝', '🍅', '🫒', '🥥', '🥑', '🍆', '🥔', '🥕', '🌽', '🌶️', '🫑', '🥒', '🥦', '🧄', '🧅', '🥜', '🍄'],
        vehicles: ['🏎️', '🚂', '🚃', '🚄', '🚅', '🚆', '🚇', '🚈', '🚝', '🚞', '🚋', '🚌', '🚎', '🚐', '🚑', '🚒', '🚓', '🚕', '🚖', '🚗', '🚙', '🛻', '🚚', '🚛', '🚜', '🏍️', '🛵', '🦽', '🦼', '🛺', '🚲', '🛴'],
    }
};

// Состояние игры
let gameState = {
    cards: [],
    flippedCards: [],
    matchedPairs: 0,
    moves: 0,
    gameStarted: false,
    startTime: null,
    timerInterval: null,
    currentTime: 0,
    size: 0, // индекс в config.sizes
    theme: 'animals', // индекс в config.themes
};

// Элементы интерфейса
let gameBoard;
let movesDisplay;
let timerDisplay;
let sizeSelect;
let themeSelect;

// Инициализация игры
function initGame() {
    resetGameState();
    renderGame();
}

// Сброс состояния игры
function resetGameState() {
    clearInterval(gameState.timerInterval);

    gameState = {
        ...gameState,
        cards: [],
        flippedCards: [],
        matchedPairs: 0,
        moves: 0,
        gameStarted: false,
        startTime: null,
        timerInterval: null,
        currentTime: 0
    };
}

// Генерация карточек
function generateCards(size, theme) {
    const pairsCount = (size * size) / 2;
    const availableEmojis = [...config.themes[theme]].slice(0, pairsCount);
    const cardPairs = [...availableEmojis, ...availableEmojis];

    // Перемешиваем карточки
    for (let i = cardPairs.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [cardPairs[i], cardPairs[j]] = [cardPairs[j], cardPairs[i]];
    }

    return cardPairs.map((emoji, index) => ({
        id: index,
        emoji,
        isFlipped: false,
        isMatched: false
    }));
}

function handleCardClick(card) {
    // Игнорируем клик если карточка уже перевернута или найдена пара
    if (card.isFlipped || card.isMatched || gameState.flippedCards.length >= 2) {
        return;
    }

    // Начинаем игру при первом клике
    if (!gameState.gameStarted) {
        startGame();
    }

    // Переворачиваем карточку
    flipCard(card);

    // Добавляем карточку в список перевернутых
    gameState.flippedCards.push(card);

    // Если перевернуто 2 карточки, проверяем на совпадение
    if (gameState.flippedCards.length === 2) {
        gameState.moves++;
        updateMovesDisplay();

        if (gameState.flippedCards[0].emoji === gameState.flippedCards[1].emoji) {
            // Карточки совпали
            gameState.flippedCards[0].isMatched = true;
            gameState.flippedCards[1].isMatched = true;
            gameState.matchedPairs++;
            gameState.flippedCards = [];

            // Проверяем завершение игры
            if (gameState.matchedPairs === (gameState.cards.length / 2)) {
                endGame();
            }
        } else {
            // Карточки не совпали - переворачиваем обратно через секунду
            setTimeout(() => {
                gameState.flippedCards.forEach(c => flipCard(c));
                gameState.flippedCards = [];
            }, 1000);
        }
    }
}

function flipCard(card) {
    card.isFlipped = !card.isFlipped;
    gameBoard.updateCards();
}

// Начало игры
function startGame() {
    gameState.gameStarted = true;
    gameState.startTime = Date.now();

    // Запускаем таймер
    gameState.timerInterval = setInterval(() => {
        gameState.currentTime = Math.floor((Date.now() - gameState.startTime) / 1000);
        updateTimerDisplay();
    }, 1000);
}

// Завершение игры
function endGame() {
    clearInterval(gameState.timerInterval);
    setTimeout(() => {
        alert(`Поздравляем! Вы нашли все пары за ${gameState.moves} ходов и ${gameState.currentTime} секунд!`);
    }, 500);
}

// Обновление отображения количества ходов
function updateMovesDisplay() {
    movesDisplay.textContent = `Ходы: ${gameState.moves}`;
}

// Обновление отображения таймера
function updateTimerDisplay() {
    timerDisplay.textContent = `Время: ${gameState.currentTime} сек`;
}

// Рендер всего интерфейса
function renderGame() {
    console.log(gameState);
    const size = config.sizes[gameState.size].value;
    const theme = gameState.theme;

    // Генерируем новые карточки
    gameState.cards = generateCards(size, theme);
    if (window.app.rerender)
        window.app.rerender()
}

function App(rerender) {
    return () => {
        return div(
            cls("game-container"),
            div(
                cls("game-header"),
                div(
                    cls("controls"),
                    div(
                        cls("form-group"),
                        "Размер сетки:",
                        sizeSelect = select(
                            ...config.sizes.map((size, index) =>
                                option(
                                    attributes({ value: index }),
                                    index === gameState.size ? attributes({ selected: "" }) : '',
                                    size.name
                                )
                            ),
                            events({
                                change: (e) => { gameState.size = Number(e.target.value); initGame(); }
                            })
                        )
                    ),
                    div(
                        cls("form-group"),
                        "Тема:",
                        themeSelect = select(
                            ...Object.keys(config.themes).map(theme =>
                                option(
                                    attributes({ value: theme }),
                                    theme === gameState.theme ? attributes({ selected: "" }) : '',
                                    theme.charAt(0).toUpperCase() + theme.slice(1)
                                )
                            ),
                            events({
                                change: (e) => { gameState.theme = e.target.value; initGame(); }
                            })
                        )
                    )
                ),
                div(
                    cls("stats"),
                    movesDisplay = div(cls("moves"), "Ходы: 0"),
                    timerDisplay = div(cls("timer"), "Время: 0 сек")
                ),
                input(
                    attributes({ type: "button", value: "Новая игра" }),
                    events({
                        click: () => initGame()
                    }),
                    cls("restart-button")
                )
            ),
            gameBoard = GameBoard(),
        );
    }
}

function GameBoard() {
    const size = config.sizes[gameState.size].value;

    const cards = gameState.cards.map((card) => Card(card, size));
    const e = div(
        cls("game-board"),
        style({
            'grid-template-columns': `repeat(${size}, 1fr)`,
            gap: "10px"
        }),
        ...cards,
    );

    e.updateCards = () => {
        cards.forEach(card => card.update())
    };

    return e;
}

function Card(card) {
    const e = div(
        cls({ card: true, matched: card.isMatched, flipped: card.isFlipped }),
        style({
        }),
        events({
            click: () => handleCardClick(card)
        }),
        div(
            cls("card-inner"),
            div(
                cls("card-front"),
                card.emoji
            ),
            div(
                cls("card-back"),
                "?"
            )
        )
    );

    e.update = () => {
        if (card.isFlipped)
            e.classList.add('flipped')
        else
            e.classList.remove('flipped')

        if (card.isMatched)
            e.classList.add('matched')
        else
            e.classList.remove('matched')
    };

    return e;
}

// Запуск игры при загрузке страницы
document.addEventListener('DOMContentLoaded', () => {
    initGame();
    slot(App, app)
});

function shuffle(array) {
    array.sort(() => Math.random() - 0.5);
}