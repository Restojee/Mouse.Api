import { JwtAuthGuard } from "../auth/guards/jwt-auth.guard";
import {
  Body,
  Controller,
  HttpStatus,
  Param,
  Post,
  UseGuards,
  Request, Get, Put
} from "@nestjs/common";
import {
  ApiBearerAuth,
  ApiOperation,
  ApiResponse,
  ApiTags
} from "@nestjs/swagger";
import { TagUpdateRequest } from "./models/tag-update-request";
import { TagCreateRequest } from "./models/tag-create-request";
import { TagService } from "./tag.service";
import { Tag } from "./models/tag";

@ApiTags('Tags')
@Controller('tags')
export class TagController {
  constructor(private tagService: TagService) {}

  @Post("create")
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth()
  @ApiResponse({ status: HttpStatus.OK, type: Tag })
  @ApiOperation({ operationId: 'CreateTag' })
  async createTagByUser(
      @Request() request,
      @Body() tagCreateRequest: TagCreateRequest,
  ) {
    const { name, description } = tagCreateRequest;
    const { id: userId } = request.user;
    return await this.tagService.createTag({ name, description, userId });
  }

  @Get("collect")
  @ApiResponse({ status: HttpStatus.OK, type: Tag })
  @ApiOperation({ operationId: 'GetTags' })
  async getTags() {
    return await this.tagService.getTags();
  }

  @Post("delete/:id")
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth()
  @ApiResponse({ status: HttpStatus.OK, type: Tag })
  @ApiOperation({ operationId: 'DeleteTag' })
  async deleteTag(@Param('id') tagId: number) {
    return await this.tagService.deleteTag({ tagId });
  }

  @Put("update")
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth()
  @ApiResponse({ status: HttpStatus.OK, type: Tag })
  @ApiOperation({ operationId: 'UpdateTag' })
  async updateTag(
      @Body() tagUpdateRequest: TagUpdateRequest,
  ) {
    const { tagId, description, name } = tagUpdateRequest;
    return await this.tagService.updateTag({ tagId, description, name });
  }
}
