#pragma once
#include <string>
#include <fstream>
#include "json.hpp"

class Car
{
private:
	std::string brand;
	std::string	model;
	std::string color;
	std::string registerNumber;
	int year;
	int price;
public:
	Car(std::string brand, std::string model, std::string color, std::string registerNumber, int year, int price);
	Car();
	Car(nlohmann::json);

	inline std::string getBrand() { return brand; }
	inline std::string	getModel() { return model; }
	inline std::string getColor() { return color; }
	inline std::string getRegisterNumber() { return registerNumber; }
	inline int getYear() { return year; }
	inline int getPrice() { return price; }

	nlohmann::json getJsonObj();

	friend std::ostream& operator<<(std::ostream& os, const Car& car) {
		return os << car.brand << " " << car.model << " " << car.color << " " << 
			car.registerNumber << " " << car.year << " " << car.price << std::endl;
	}
};

