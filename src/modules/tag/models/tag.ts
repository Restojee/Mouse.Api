import { User } from "../../user/models/User";
import { ApiProperty } from "@nestjs/swagger";
import {Exclude, Expose} from "class-transformer";

@Exclude()
export class Tag {

  @Expose()
  @ApiProperty()
  id?: number;

  @Expose()
  @ApiProperty()
  text?: string;

  @Expose()
  @ApiProperty()
  user?: User;

  @Expose()
  @ApiProperty()
  createdAt?: Date;

  @Expose()
  @ApiProperty()
  updatedAt?: Date;
}
