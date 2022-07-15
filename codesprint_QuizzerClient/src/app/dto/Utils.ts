export class Utils {
  public static hasLetter(value: string): boolean {
    return /[a-zA-Z]/.test(value);
  }

  public static hasSpecialCharacter(value: string): boolean {
    return /[^a-zA-Z0-9]/.test(value);
  }

  public static hasDigit(value: string): boolean {
    return /[0-9]/.test(value);
  }

  public static capitalize(value: string, sep: string): string {
    let strings: string[] = value.split(sep);

    strings.forEach((str, i) => {
      if (str.trim() === "") return;
      strings[i] = str[0].toUpperCase() + str.substring(1).toLowerCase();
    });

    return strings.join(sep);
  }

  public static isEmailValid(value: string): boolean {
    return /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      .test(value);
  }
}
