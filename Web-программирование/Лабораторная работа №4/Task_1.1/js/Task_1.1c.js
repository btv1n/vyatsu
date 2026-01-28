console.log(x);
var x = 10;

// Вывод: undefined
// var "поднимает" объявление переменной, но значение не ставит


console.log(y);
let y = 20;

// Вывод: Task_1.1c.js:5 Uncaught ReferenceError: Cannot access 'y' before initialization
// (anonymous) @ Task_1.1c.js:5
// let "поднимает", но запрещает использовать переменную до ее объявления в коде


// "Поднимает" (Hoisting) - "помнит о переменной с самого начала, но значение даёт только когда доходит до строки с присваиванием".