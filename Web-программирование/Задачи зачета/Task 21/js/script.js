const arr = [
	{ name: "Alice", age: 25, job: "developer" },
	{ name: "Bob", age: 23, job: "developer" },
	{ name: "Eva", age: 22, job: "tester" },
	{ name: "Mike", age: 26, job: "admine" }
]

const output = document.getElementById('output')

let html = '<ul>'

arr.forEach(person => {
	html += `
          <li>
              ${person.name}
              <ul>
                  <li>age - ${person.age}</li>
                  <li>job - ${person.job}</li>
              </ul>
          </li>
      `
})

html += '</ul>'

output.innerHTML = html