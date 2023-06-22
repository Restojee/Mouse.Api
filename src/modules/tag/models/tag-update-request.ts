import { ApiProperty } from "@nestjs/swagger";
import { TagCreateRequest } from "./tag-create-request";

export class TagUpdateRequest extends TagCreateRequest {
    @ApiProperty()
    tagId: number;
}