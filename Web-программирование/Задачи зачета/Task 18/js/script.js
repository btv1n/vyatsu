const data = [
	{ "name": "Alex", "age": 30, "job": "developer" },
	{ "name": "Sofa", "age": 22, "job": "coach" },
	{ "name": "Ilya", "age": 26, "job": "developer" },
	{ "name": "Kolya", "age": 33, "job": "administrator" }
]

// Функция группировки по ключу job
function groupByJob(arr) {
	const result = {} // словарь

	arr.forEach(person => {
		// достаем поля из объекта
		const { name, age, job } = person

		// если такого job нет - создаём пустой массив
		if (!result[job]) {
			result[job] = []
		}
		// если есть job - добавляем в существующий

		// добавляем объект с нужными полями
		result[job].push({ name, age })
	})

	return result
}

const grouped = groupByJob(data) // словарь
console.log(grouped) // вывод словаря

// console.log(grouped['developer'])