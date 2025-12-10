package Commands;

public class CommandResult {
    public final CommandStatus status;
    public final String message;

    public CommandResult(CommandStatus status, String message) {
        this.status = status;
        this.message = message;
    }

    public CommandResult(CommandStatus status) {
        this.status = status;
        this.message = "";
    }
}
