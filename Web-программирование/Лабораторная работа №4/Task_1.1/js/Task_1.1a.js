function testVar() {
	if (true) {
		var x = 10;
	}
	console.log(x);
}

function testLet() {
	if (true) {
		let y = 20;
	}
	console.log(y);
}


/* Task_1.1.js:5 10 
Выведет 10, потому что var имеет функциональную область видимости, т.е.
переменная видна во всей функции*/
testVar();


/* Task_1.1.js:12 Uncaught ReferenceError: y is not defined 
Вызовет ошибку ReferenceError, потому что let имеет блочную область видимости,
т.е. виден только внутри блока*/
testLet();