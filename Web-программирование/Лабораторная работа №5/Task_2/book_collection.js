import { attributes, div, events, input, li, option, select, slot, style, ul } from "./elements.mjs"

// Theme definitions
const themes = {
    light: {
        '--bg-color': '#ffffff',
        '--text-color': '#333333',
        '--primary-color': '#4a6fa5',
        '--secondary-color': '#f0f0f0',
        '--border-color': '#dddddd'
    },
    dark: {
        '--bg-color': '#1a1a1a',
        '--text-color': '#f0f0f0',
        '--primary-color': '#6b8cae',
        '--secondary-color': '#2d2d2d',
        '--border-color': '#444444'
    },
    colorful: {
        '--bg-color': '#f9f7f0',
        '--text-color': '#5a3d2b',
        '--primary-color': '#e17055',
        '--secondary-color': '#fdcb6e',
        '--border-color': '#e17055'
    }
};

// Apply theme function
function applyTheme(themeName) {
    const theme = themes[themeName];
    const root = document.documentElement;
    Object.entries(theme).forEach(([key, value]) => {
        root.style.setProperty(key, value);
    });
}

function ThemeSwitcher() {
    const themeSelect = select(
        ...Object.keys(themes).map((name) =>
            option(
                attributes({ value: name }),
                name.charAt(0).toUpperCase() + name.slice(1),
            )
        ),
        events({
            change: (e) => {
                applyTheme(e.target.value);
            }
        }),
        style({
            padding: '8px 12px',
            border: '1px solid var(--border-color)',
            'border-radius': '4px',
            'background-color': 'var(--bg-color)',
            color: 'var(--text-color)',
            cursor: 'pointer',
            margin: '10px 0'
        })
    );

    return div(
        style({
            display: 'flex',
            justifyContent: 'center',
            margin: '20px 0'
        }),
        themeSelect
    );
}

function Controls(add) {
    const authorsInput = input(
        attributes({ placeholder: 'Авторы' }),
        style({
            padding: '8px',
            margin: '5px 0',
            width: '100%',
            'box-sizing': 'border-box'
        })
    );

    const nameInput = input(
        attributes({ placeholder: 'Название книги' }),
        style({
            padding: '8px',
            margin: '5px 0',
            width: '100%',
            'box-sizing': 'border-box'
        })
    );

    const button = input(
        attributes({ type: 'button', value: 'Добавить' }),
        events({ click: () => { add(authorsInput.value, nameInput.value) } }),
        style({
            padding: '10px 15px',
            margin: '10px 0',
            "background-color": 'var(--primary-color)',
            color: 'white',
            border: 'none',
            "border-radius": '4px',
            cursor: 'pointer',
            width: '100%'
        })
    );

    return div(
        style({
            'max-width': '400px',
            margin: '0 auto',
            padding: '20px',
            "background-color": 'var(--secondary-color)',
            "border-radius": '8px'
        }),
        div(
            style({ 'margin-bottom': '15px' }),
            authorsInput
        ),
        div(
            style({ 'margin-bottom': '15px' }),
            nameInput
        ),
        div(button)
    );
}

function List(list) {
    return () =>
        ul(
            style({
                'max-width': '600px',
                margin: '20px auto',
                display: 'grid',
                'grid-template-columns': 'repeat(auto-fill, minmax(250px, 1fr))',
                gap: '15px',
            }),
            ...list.map((b) =>
                li(
                    style({
                        padding: '15px',
                        "background-color": 'var(--secondary-color)',
                        "border-radius": '6px',
                        border: '1px solid var(--border-color)'
                    }),
                    div(
                        style({
                            'font-weight': 'bold',
                            'margin-bottom': '8px',
                            color: 'var(--primary-color)'
                        }),
                        b[1] // Book name
                    ),
                    div(
                        style({
                            'font-size': '0.9em',
                            color: 'var(--text-color)'
                        }),
                        b[0] // Authors
                    )
                )
            )
        );
}

function App(rerender) {
    const list = [['Лев Толстой', 'Война и мир'], ['Фёдор Достоевский', 'Преступление и наказание']];
    const controls = Controls(add);
    const listC = List(list);
    const themeSwitcher = ThemeSwitcher();

    function add(authors, name) {
        if (authors && name) {
            list.push([authors, name]);
            rerender();
        }
    }

    return () => div(
        themeSwitcher,
        controls,
        listC()
    );
}

applyTheme('light');
slot(App, app);