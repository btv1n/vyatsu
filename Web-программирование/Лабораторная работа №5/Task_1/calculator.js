import { attributes, cls, div, events, input, mount } from './elements.mjs'

function Button(text, className, onClick = () => { }) {
    return input(
        cls(className),
        attributes({
            value: text,
            type: 'button',
        }),
        events({
            click: onClick
        }),
    );
}

function Calculator() {
    let expression = '';
    let lastResult = '';

    const historyDisplay = input(cls('display'), attributes({ readOnly: true, type: 'text' }));
    const resultDisplay = input(cls('result'), attributes({ readOnly: true, type: 'text' }));

    function updateDisplays() {
        historyDisplay.value = expression;
        resultDisplay.value = lastResult || '0';
    }

    function clear() {
        expression = '';
        lastResult = '';
        updateDisplays();
    }

    function appendToExpression(value) {
        const lastChar = expression.slice(-1);
        if (['+', '-', '×', '÷'].includes(lastChar) && ['+', '-', '×', '÷'].includes(value)) {
            expression = expression.slice(0, -1) + value;
        } else {
            expression += value;
        }
        updateDisplays();
    }

    function calculate() {
        try {
            if (!expression) return;

            const calcExpression = expression.replace(/×/g, '*').replace(/÷/g, '/');
            lastResult = new Function(`return ${calcExpression}`)();

            updateDisplays();
        } catch (e) {
            lastResult = 'Error';
            updateDisplays();
        }
    }

    function binaryToDecimal() {
        try {
            if (!expression) return;

            // Проверяем, что в выражении только 0 и 1
            if (/^[01]+$/.test(expression)) {
                lastResult = parseInt(expression, 2).toString();
                updateDisplays();
            } else {
                lastResult = 'Not binary';
                updateDisplays();
            }
        } catch (e) {
            lastResult = 'Error';
            updateDisplays();
        }
    }

    function decimalToBinary() {
        try {
            if (!expression) return;

            // Проверяем, что в выражении только цифры
            if (/^\d+$/.test(expression)) {
                const num = parseInt(expression, 10);
                lastResult = num.toString(2);
                updateDisplays();
            } else {
                lastResult = 'Not decimal';
                updateDisplays();
            }
        } catch (e) {
            lastResult = 'Error';
            updateDisplays();
        }
    }

    const calcButtons = [
        Button('7', 'number', () => appendToExpression('7')),
        Button('8', 'number', () => чappendToExpression('8')),
        Button('9', 'number', () => appendToExpression('9')),
        Button('÷', 'operator', () => appendToExpression('÷')),
        Button('4', 'number', () => appendToExpression('4')),
        Button('5', 'number', () => appendToExpression('5')),
        Button('6', 'number', () => appendToExpression('6')),
        Button('×', 'operator', () => appendToExpression('×')),
        Button('1', 'number', () => appendToExpression('1')),
        Button('2', 'number', () => appendToExpression('2')),
        Button('3', 'number', () => appendToExpression('3')),
        Button('-', 'operator', () => appendToExpression('-')),
        Button('.', 'decimal', () => {
            const parts = expression.split(/[\+\-\*\/]/);
            const lastPart = parts[parts.length - 1];
            if (!lastPart.includes('.')) {
                appendToExpression(value);
            }
        }),
        Button('0', 'number', () => appendToExpression('0')),
        Button('=', 'equals', calculate),
        Button('+', 'operator', () => appendToExpression('+')),
        Button('C', 'clear', clear)
    ];

    const converterButtons = [
        Button('Bin → Dec', 'converter', binaryToDecimal),
        Button('Dec → Bin', 'converter', decimalToBinary)
    ];

    const calcButtonsContainer = div(cls('buttons'), ...calcButtons);
    const converterButtonsContainer = div(cls('converter-buttons'), ...converterButtons);

    return () => div(
        div(cls("title"), "Калькулятор"),
        historyDisplay,
        resultDisplay,
        calcButtonsContainer,
        converterButtonsContainer
    );
}

mount('calc', Calculator());