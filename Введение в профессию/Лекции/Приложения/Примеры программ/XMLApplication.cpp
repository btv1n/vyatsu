#include <iostream>
#include <vector>
#include <string>
#include "pugixml-1.13/src/pugixml.hpp"
#include "../StaticLib/Car.h"

using namespace std;
using namespace pugi;

vector<Car> getCars(pugi::xml_document const & doc);
void print(vector<Car>);
void printfile(string, vector<Car>);

int main()
{
    xml_document doc;
    xml_parse_result result = doc.load_file("cars.xml");
    if (!result)
        return -1;

    vector<Car> cars = getCars(doc);
    //print(cars);
    printfile("newcars.xml", cars);

}

vector<Car> getCars(pugi::xml_document const& doc)
{
    vector<Car> cars;
    for (xml_node node : doc.child("cars").children("car"))
    {
        string brand = node.child_value("brand");
        string model = node.child_value("model");
        string color = node.child_value("color");
        string registerNumber = node.attribute("registerNumber").as_string();
        int year = stoi(node.child_value("year"));
        int price = stoi(node.child_value("price"));
        Car car(brand, model, color, registerNumber, year, price);
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

void printfile(string fileName, vector<Car> cars)
{
    xml_document doc;
    xml_node cars_node = doc.append_child("cars");
    for (Car car : cars)
    {
        xml_node car_node = cars_node.append_child("car");
        xml_attribute attr = car_node.append_attribute("registerNumber");
        attr.set_value(car.getRegisterNumber().c_str());

        xml_node brand_node = car_node.append_child("brand");
        brand_node.text().set(car.getBrand().c_str());

        xml_node model_node = car_node.append_child("model");
        model_node.text().set(car.getModel().c_str());
        
        xml_node color_node = car_node.append_child("color");
        color_node.text().set(car.getColor().c_str());

        xml_node year_node = car_node.append_child("year");
        year_node.text().set(to_string(car.getYear()).c_str());

        xml_node price_node = car_node.append_child("price");
        price_node.text().set(to_string(car.getPrice()).c_str());
    }
    doc.save_file(fileName.c_str());
}
