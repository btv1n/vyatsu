package Commands;

import java.io.File; // класс для работы с файлами и каталогами
import java.util.Arrays;

import Console.AnsiColors;
import Wd.WorkingDirectory;

import org.slf4j.*;

// Класс реализует команду вывода текущего каталога и его подкаталогов
// параметр -t показывает вложенные папки как дерево

// implements - класс реализует интерфейс command
public class ListCommand implements Command {
    // Логирование
    private static final Logger log = LoggerFactory.getLogger(ListCommand.class);

    // описание команды, выводимое при вызове help
    private static final String description = "\tВыводит список содержимого текущего каталога\n\t[-t] рекурсивно выводит дерево каталогов \n\t[-h] цветовая подсветка";

    @Override
    public CommandResult execute(String[] args) {
        log.debug("Выполнение ListCommand.");
        boolean h = false;
        boolean tree = false;
        // переберает аргументы начиная со второго, если аргумент не известен, то выводит ошибку
        for (int i = 1; i < args.length; i++) {
            String arg = args[i];
            switch (arg) {
                case "-h":
                    h = true;
                    break;
                case "-t":
                    tree = true;
                    break;
                default:
                    log.error("CommandResult - передан неизвестнвый аргумент - {}", arg);
                    return new CommandResult(CommandStatus.ERROR, "Неизвестный аргумент: \"" + arg + "\"");
            }
        }

        // получает текущий рабочий каталог
        WorkingDirectory wd = WorkingDirectory.getInstance();

        // выводит содержимое
        list(wd.getPath(), h, tree, 0);

        // возвращает результат со статусом успешно
        log.debug("ListCommand выполнена успешно.");
        return new CommandResult(CommandStatus.SUCCESS);
    }

    // рекурсивный метод вывод содержимого каталога
    // path - путь к каталогу
    // tree - показывать ли рекурсивно
    // depth - текущая глубина вложенности, нужна для отступов при выводе дерева
    private void list(String path, boolean h, boolean tree, int depth) {
        log.debug("Выполнение list.");
        File dir = new File(path);
        File[] files = dir.listFiles();

        // сортирует список файлов по имени
        Arrays.sort(files, (a, b) -> a.getName().compareTo(b.getName()));

        // перебирает каждый элемент в каталоге
        for (File fileEntry : files) {
            String name = fileEntry.getName();

            // подсветка каталога синим цветом
            if (h && fileEntry.isDirectory()) {
                name = AnsiColors.ANSI_CYAN + name + AnsiColors.ANSI_RESET;
            }

            // добавляются отступы перед именем в зависимости от глубины вложенности
            for (int i = 0; i < depth; i++) {
                System.out.print("  ");
            }

            // печать имени файла или папки
            System.out.println(name);

            // если активен флаг -t и элемент - это каталог, то рекурсивно вызывается list()
            // и увеличивается уровень вложенности depth + 1
            // в результате будет выполнен обход всех каталогов
            if (tree && fileEntry.isDirectory()) {
                list(fileEntry.getAbsolutePath(), h, tree, depth + 1);
            }
            log.debug("list выполнена успешно.");
        }
    }

    // возвращает текстовое описание программы, используется при вызове help
    @Override
    public String getDescription() {
        return description;
    }
}
