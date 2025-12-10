package CommandTests;

import Commands.RemoveDirectoryCommand;
import Commands.CommandResult;
import Commands.CommandStatus;
import Wd.WorkingDirectory;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

import java.io.File;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.*;

class RemoveDirectoryCommandTest {

    // Аннотация @TempDir создает временную директорию для каждого теста
    @TempDir
    Path tempDir;

    @Test
    void testRemoveDirectory() {
        // создание директории
        new File(tempDir.toFile(), "toremove").mkdir();

        RemoveDirectoryCommand command = new RemoveDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        // удаление созданной директории
        String[] args = {"rmdir", "toremove"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.SUCCESS, result.status);

        // создание File объект для пути, который удален
        File removed = new File(tempDir.toFile(), "toremove");

        // проверка, что директории больше не существует
        assertFalse(removed.exists());
    }

    @Test
    void testRemoveNonExistent() {
        RemoveDirectoryCommand command = new RemoveDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        // несуществующие аргументы
        String[] args = {"rmdir", "nonexistent"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }

    @Test
    void testRemoveFileInsteadOfDirectory() throws Exception {
        // создание файла
        new File(tempDir.toFile(), "file.txt").createNewFile();

        RemoveDirectoryCommand command = new RemoveDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        // попытка удаление файла вместо директории
        String[] args = {"rmdir", "file.txt"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }

    @Test
    void testNoArguments() {
        RemoveDirectoryCommand command = new RemoveDirectoryCommand();

        // выполнение команды без аргументов
        String[] args = {"rmdir"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }
}
