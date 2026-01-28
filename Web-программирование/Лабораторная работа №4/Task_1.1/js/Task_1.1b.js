/*
var   a = 10;
let   b = 20;
const c = 30;

a = 15; // значения у var можно переприсваивать
b = 25; // значения у let можно переприсваивать
c = 35; // значения у const нельзя переприсваивать, ошибка: Task_1.1b.js:7 Uncaught TypeError: Assignment to constant variable.

console.log(a,b,c);
*/

// Изменение содержимого объекта с const
const object = {
	name: "Name",
	age: 55
};
console.log("Before: ", object);

object.name = "Name2";
object.age  = 66;
console.log("After: ", object);