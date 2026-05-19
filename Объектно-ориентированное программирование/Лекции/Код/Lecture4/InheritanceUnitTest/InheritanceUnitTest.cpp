#include "pch.h"
#include "CppUnitTest.h"
#include "../InheritanceStaticLib/Person.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace InheritanceUnitTest
{
	TEST_CLASS(InheritanceUnitTest)
	{
	public:
		
		TEST_METHOD(ConstructorCorrectAgePerson)
		{
			Person p("Ivanov I.I.", 18);
			Assert::AreEqual(p.getAge(), 18);
		}
		TEST_METHOD(ConstructorInCorrectAgePerson)
		{
			auto func = [] {Person p("Ivanov I.I.", -18); };
			Assert::ExpectException<std::exception>(func);
		}

	};
}
