function getMostPopularCity(arr) {
  const citiesCount = {}; // объект для подсчета количества людей в каждом городе

	// проходимся по массиву
  for (const person of arr) {
    const city = person.from; // берем город текущего человека
		// если такой город есть - текущее значение + 1, иначе 0 + 1
    citiesCount[city] = (citiesCount[city] || 0) + 1;
  }

  let maxCity = null; // переменная для хранения города с максимальным количеством людей
  let maxCount = 0; // счетчик

	// проходим по объекту с подсчетом городом и определяем максимум
  for (const city in citiesCount) {
    if (citiesCount[city] > maxCount) {
      maxCount = citiesCount[city];
      maxCity = city;
    }
  }

  return maxCity;
}

const data = [
  { name: 'Vova', age: 25, from: 'Moscow' },
  { name: 'Anna', age: 30, from: 'Berlin' },
  { name: 'Alina Rin', age: 36, from:'Tokio'},
  { name: 'Olga', age: 28, from: 'Paris' },
  { name: 'Max', age: 35, from: 'Moscow' }
];


console.log(getMostPopularCity(data)); // Moscow