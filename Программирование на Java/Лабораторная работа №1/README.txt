После установки JDK его нужно внести в переменные среды и добавить в проект:

1) Cкачать и установить JDK: https://www.oracle.com/java/technologies/javase/jdk22-archive-downloads.html -> Windows x64 Installer
2) Системные переменные -> Имя переменной: JAVA_HOME, Значение переменной: C:\Program Files\Java\jdk-22 ;
3) В переменную Path добавить: %JAVA_HOME%\bin
4) Перейти: File -> Project Structure -> Project -> SDK - выбрать установленный JDK -> Apply -> OK

Чтобы установить JUnit нужно:

1) Перейти: File -> Project Structure -> Libraries -> (+) From Maven -> установить junit.jupiter -> Apply -> OK
2) Обновить файл с тестами