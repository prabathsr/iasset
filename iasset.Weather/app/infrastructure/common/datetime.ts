export class DateTime {
    public static getTimeStamp(unixstamp: number): number {
        return unixstamp * 1000;
    }
}