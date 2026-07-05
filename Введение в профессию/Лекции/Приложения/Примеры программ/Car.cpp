#include "pch.h"
#include "Car.h"

Car::Car(std::string brand, std::string model, std::string color, std::string registerNumber, int year, int price):
	brand(brand), model(model), color(color), registerNumber(registerNumber), year(year), price(price) {}
Car::Car():
	brand(""), model(""), color(""), registerNumber(""), year(0), price(0) {}
Car::Car(nlohmann::json jcar)
{
	brand = jcar["brand"].get<std::string>();
	model = jcar["model"].get<std::string>();
	color = jcar["color"].get<std::string>();
	registerNumber = jcar["registerNumber"].get<std::string>();
	year = jcar["year"].get<int>();
	price = jcar["price"].get<int>();
}
nlohmann::json Car::getJsonObj()
{
	nlohmann::json carObj;
	carObj["brand"] = brand; // запись в Json-объект полей структуры
	carObj["model"] = model;
	carObj["color"] = color;
	carObj["registerNumber"] = registerNumber;
	carObj["year"] = year;
	carObj["price"] = price;
	return carObj;
}