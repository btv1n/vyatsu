import java.util.HashMap;

import Commands.ChangeDirectoryCommand;
import Commands.Command;
import Commands.CommandResult;
import Commands.CommandStatus;
import Commands.HelpCommand;
import Commands.ListCommand;
import Commands.MakeDirectoryCommand;
import Commands.RemoveDirectoryCommand;
import Console.AnsiColors;
import Wd.WorkingDirectory;

// логи
import org.slf4j.*;
// использование - log.trace("Конструктор завершен с переменной name={}", name)
// log.info("Программа запущена.");
// log.debug("1");
// log.error("2");
// log.warn("3");

// уровни важности в логировании
// Trace - детальная отладка
// Debug - отладочная информация
// Info  - информационное сообщение о нормальной работе приложения
// Warn  - предупреждение
// Error - ошибка, исключения
// Fatal - критическая ошибка

// Тесты

public class Main {
    // Логирование
    private static final Logger log = LoggerFactory.getLogger(Main.class);

    // хеш-таблица для хранения комманд
    static HashMap<String, Command> commands = new HashMap<>();
    // статический блок инициализации
    static {
        // 1) просматривать содержимое только данного каталога
        // 2) (на хорошо) просматривать содержимое данного каталога вместе с содержимым его подкаталогов в виде дерева
        commands.put("ls", new ListCommand());

        // 3) переходить к родительскому каталогу
        // -> cd ..
        // 4) переходить к дочернему каталогу, указав имя дочернего каталога
        // -> cd <имя_каталога> ; поддерживает абсолютные пути (/home/user) и относительные (Documents)
        commands.put("cd", new ChangeDirectoryCommand());

        // 5) (на хорошо) создавать каталог с именем
        commands.put("mkdir", new MakeDirectoryCommand());

        // 6) (на отлично) удалять каталог с именем, причем каталог удаляется вместе с его содержимым
        commands.put("rmdir", new RemoveDirectoryCommand());

        // вывод всех доступных команд
        // передаем существующую карту commands в конструктор HelpCommand
        commands.put("help", new HelpCommand(commands));
    }

    public static void main(String[] args) {
        log.info("Программа запущена.");
        boolean running = true;

        while (running) {
            // получает текущий рабочий каталог - singleton
            WorkingDirectory wd = WorkingDirectory.getInstance();

            // выводит путь (C:\Users\>
            System.out.print(wd.getPath() + "> ");

            // передает введенную строку в метод handleInput
            handleInput(System.console().readLine());
        }
        log.info("Программа завершена.");
    }

    // проверяет, что пользователь что-то ввел
    private static void handleInput(String input) {
        log.debug("Ввод от пользователя.");
        if (input.length() == 0)
            return;

        // разбивает введенную строку по пробелам ; первый элемент - команда, остальные - аргументы
        String[] args = input.split(" ");

        // ищет команду по названию в hashmap, если не найдена, то null
        Command command = commands.getOrDefault(args[0], null);
        if (command != null) {
            // команда найдена, она выполняется
            CommandResult result = command.execute(args);
            // произошла ошибка при выполнении - она выводится красным цветом
            if(result.status == CommandStatus.ERROR) {
                log.error("Ошибка при выполении handleInput - {}", result.message);
                System.out.println(AnsiColors.ANSI_RED + "Ошибка: " + result.message + AnsiColors.ANSI_RESET);
            }
        } else {
            log.error("Ошибка при выполении handleInput - команда не найдена");
            System.out.println(AnsiColors.ANSI_RED + "Ошибка: команда не найдена!" + AnsiColors.ANSI_RESET);
        }
    }
}
