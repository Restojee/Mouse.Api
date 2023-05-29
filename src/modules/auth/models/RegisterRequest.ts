import {ApiProperty} from "@nestjs/swagger";
import {MaxLength, MinLength} from "class-validator";

export class RegisterRequest {

    @MinLength(3, { message: 'Логин должен состояить не менее чем из 3 символов' })
    @MaxLength(15, { message: 'Логин должен состоять не более чем из 15 символов' })
    @ApiProperty()
    username: string;

    @MinLength(8, { message: 'Пароль должен состоять не менее чем из 8 символов' })
    @MaxLength(20, { message: 'Логин должен состоять не более чем из 20 символов' })
    @ApiProperty()
    password: string;
}