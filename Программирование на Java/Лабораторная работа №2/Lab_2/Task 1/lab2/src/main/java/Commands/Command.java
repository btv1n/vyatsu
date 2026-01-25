package Commands;

public interface Command {
    public CommandResult execute(String[] args);
    public String getDescription();
}
