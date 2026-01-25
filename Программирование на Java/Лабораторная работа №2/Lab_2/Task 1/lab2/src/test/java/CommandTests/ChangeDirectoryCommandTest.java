package CommandTests;

import Commands.ChangeDirectoryCommand;
import Commands.CommandResult;
import Commands.CommandStatus;
import Wd.WorkingDirectory;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

import java.io.File;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.*;

class ChangeDirectoryCommandTest {

    // временная директория для изоляции тестов
    @TempDir
    Path tempDir;

    @Test
    void testChangeToSubdirectory() {
        new File(tempDir.toFile(), "testdir").mkdir();

        ChangeDirectoryCommand command = new ChangeDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        String[] args = {"cd", "testdir"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.SUCCESS, result.status);
    }

    @Test
    void testChangeToParent() {
        // создание поддиректории
        File subdir = new File(tempDir.toFile(), "subdir");
        subdir.mkdir();

        ChangeDirectoryCommand command = new ChangeDirectoryCommand();
        // устанавливается рабочая директория внутри поддиректории
        WorkingDirectory wd = WorkingDirectory.getInstance(subdir.getAbsolutePath());

        String[] args = {"cd", ".."};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.SUCCESS, result.status);
    }

    @Test
    void testChangeToNonExistent() {
        ChangeDirectoryCommand command = new ChangeDirectoryCommand();
        WorkingDirectory wd = WorkingDirectory.getInstance(tempDir.toString());

        // попытка перехода в несуществующую директорию
        String[] args = {"cd", "nonexistent"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }

    @Test
    void testNoArguments() {
        ChangeDirectoryCommand command = new ChangeDirectoryCommand();

        // вызов команды без аргументов
        String[] args = {"cd"};
        CommandResult result = command.execute(args);

        assertEquals(CommandStatus.ERROR, result.status);
    }
}