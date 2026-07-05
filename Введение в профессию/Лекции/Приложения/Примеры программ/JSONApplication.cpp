#include <iostream>
#include <vector>
#include <fstream>
#include "json.hpp"
#include "../StaticLib/Car.h"

using namespace std;
using nlohmann::json;

vector<Car> create();
vector<json> jsonObjFromCars(vector<Car>);
void saveFile(string, json);
json initfile(string fileName);
vector<Car> fromJsonObj(json);
void print(vector<Car>);

int main()
{
	json obj;
	vector<Car> cars = create();
	vector<json> carObj = jsonObjFromCars(cars);
	obj["cars"] = carObj;
	saveFile("cars.json", obj);

	json newObj = initfile("cars.json");
	vector<Car> newCars = fromJsonObj(newObj);
	print(newCars);
}

vector<Car> create()
{
	vector<Car> cars;
	cars.push_back(Car("Ford", "Mustang", "Red", "ADF-1121", 2021, 59000));
	cars.push_back(Car("Nissan", "Leaf", "White", "SSJ-3002", 2019, 29000));
	cars.push_back(Car("Toyota", "Prius", "Silver", "KKO-0212", 2020, 39000));
	return cars;
}

vector<json> jsonObjFromCars(vector<Car> cars)
{
	vector<json> carsJson;
	for (Car car : cars)
	{
		carsJson.push_back(car.getJsonObj());
	}
	return carsJson;
}

void saveFile(string fileName, json jcars)
{
	ofstream fout(fileName);
	fout << jcars;
	fout.close();
}

json initfile(string fileName)
{
	ifstream fin(fileName);
	json jcar = json::parse(fin); //fin >> jcar;
	fin.close();
	return jcar;
}

vector<Car> fromJsonObj(json jobj)
{
	vector<json> jCars = jobj["cars"].get<vector<json>>();
	vector<Car> cars;
	for (json jcar : jCars)
	{
		Car car(jcar);
		cars.push_back(car);
	}
	return cars;
}

void print(vector<Car> cars)
{
	for (Car car : cars)
	{
		cout << car;
	}
}
