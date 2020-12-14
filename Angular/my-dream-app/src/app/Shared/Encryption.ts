export class Encryption {
  static Encrypt(token: string): string {
    let returnVal = '';

    for (let index = 0; index < token.length; index++) {
      returnVal += String.fromCharCode(token.charCodeAt(index) + 5);
    }

    return returnVal;
  }

  static Decrypt(token: string): string {
    let returnVal = '';

    for (let index = 0; index < token.length; index++) {
      returnVal += String.fromCharCode(token.charCodeAt(index) - 5);
    }

    return returnVal;
  }
}
