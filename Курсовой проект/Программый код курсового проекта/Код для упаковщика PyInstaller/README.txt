pyinstaller --onefile --add-data "data;data" main.py

pyinstaller --onefile --noconsole --add-data "data;data" main.py

--onefile — собирает один .exe
--noconsole — убирает черное консольное окно при запуске Tkinter
--add-data "data;data" — добавляет папку с исходными данными