import { ApiProperty } from "@nestjs/swagger";

export class CreateUserRequest {

    @ApiProperty()
    id: number;

    @ApiProperty()
    username?: string;

    @ApiProperty()
    password?: string;
}
